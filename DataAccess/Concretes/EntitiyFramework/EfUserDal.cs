using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstracts;
using Entities.Dtos.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfUserDal : EfRepositoryBase<Context, User>, IUserDal
    {
        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context = new Context())
            {
                var result = context.Set<UserOperationClaim>().Where(u=> u.UserId == user.Id).Include(c=> c.OperationClaim).Select(c=> c.OperationClaim).ToListAsync();
                return await result;
            }
        }

        public async Task<List<User>> GetUsersByQueryAsync(UserQueryDto query)
        {
            using var context = new Context();
            // Veritabanı sorgusunu başlatıyoruz
            var usersQuery = context.Users.Include(u => u.OperationClaims).ThenInclude(uoc => uoc.OperationClaim).AsNoTracking().AsQueryable();

            // Filtreleme işlemleri
            if (query.Id.HasValue)
            {
                usersQuery = usersQuery.Where(u=> u.Id == query.Id);
            }
            if (!string.IsNullOrEmpty(query.FirstName))
            {
                usersQuery = usersQuery.Where(u => EF.Functions.Like(u.FirstName, $"%{query.FirstName}%"));
            }

            if (!string.IsNullOrEmpty(query.LastName))
            {
                usersQuery = usersQuery.Where(u => EF.Functions.Like(u.LastName, $"%{query.LastName}%"));
            }

            if (!string.IsNullOrEmpty(query.NationalityId))
            {
                usersQuery = usersQuery.Where(u => u.NationalityId.Contains(query.NationalityId));
            }

            if (!string.IsNullOrEmpty(query.Email))
            {
                usersQuery = usersQuery.Where(u => EF.Functions.Like(u.Email, $"%{query.Email}%"));
            }

            if (query.MinDateOfBirth.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.DateOfBirth >= query.MinDateOfBirth.Value.Date);
            }

            if (query.MaxDateOfBirth.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.DateOfBirth <= query.MaxDateOfBirth.Value.Date);
            }

            if (query.Status.HasValue)
            {
                usersQuery = usersQuery.Where(u=> u.Status == query.Status);
            }

            if (!string.IsNullOrEmpty(query.OperationClaimId))
            {
                usersQuery = usersQuery.Where(u =>
                    u.OperationClaims.Any(uoc =>
                        uoc.OperationClaim.Id.ToString() == query.OperationClaimId));
            }

            if (!string.IsNullOrEmpty(query.OperationClaimName))
            {
                usersQuery = usersQuery.Where(u =>
                    u.OperationClaims.Any(uoc =>
                        EF.Functions.Like(uoc.OperationClaim.Name, $"%{query.OperationClaimName}%")));
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                usersQuery = usersQuery.Where(u =>
                    EF.Functions.Like(u.FirstName, $"%{query.SearchTerm}%") ||
                    EF.Functions.Like(u.LastName, $"%{query.SearchTerm}%") ||
                    EF.Functions.Like(u.NationalityId, $"%{query.SearchTerm}%") ||
                    EF.Functions.Like(u.Id.ToString(), $"%{query.SearchTerm}%")
                );
            }

            // Sıralama işlemleri
            if (query.SortBy.ToLower() == "firstname")
            {
                usersQuery = query.IsDescending ? usersQuery.OrderByDescending(u => u.FirstName) : usersQuery.OrderBy(u => u.FirstName);
            }
            else if (query.SortBy.ToLower() == "lastname")
            {
                usersQuery = query.IsDescending ? usersQuery.OrderByDescending(u => u.LastName) : usersQuery.OrderBy(u => u.LastName);
            }
            else if (query.SortBy.ToLower() == "nationalityid")
            {
                usersQuery = query.IsDescending ? usersQuery.OrderByDescending(u => u.NationalityId) : usersQuery.OrderBy(u => u.NationalityId);
            }
            else if (query.SortBy.ToLower() == "email")
            {
                usersQuery = query.IsDescending ? usersQuery.OrderByDescending(u => u.Email) : usersQuery.OrderBy(u => u.Email);
            }
            else if (query.SortBy.ToLower() == "dateofbirth")
            {
                usersQuery = query.IsDescending ? usersQuery.OrderByDescending(u => u.DateOfBirth) : usersQuery.OrderBy(u => u.DateOfBirth);
            }
            else
            {
                usersQuery = query.IsDescending ? usersQuery.OrderByDescending(u => u.Id) : usersQuery.OrderBy(u => u.Id);
            }

            // Sayfalama işlemleri
            usersQuery = usersQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            // Asenkron olarak veritabanından sorguyu çalıştırıyoruz
            return await usersQuery.ToListAsync();  // Burada ToListAsync() kullanarak veritabanından sonuçları alıyoruz.
        }


    }
}
