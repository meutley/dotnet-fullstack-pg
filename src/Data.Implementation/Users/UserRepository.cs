using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using SourceName.Data.Model;
using SourceName.Data.Model.Roles;
using SourceName.Data.Model.Users;
using SourceName.Data.Users;

namespace SourceName.Data.Implementation.Users
{
    public class UserRepository : IntegerRepositoryBase<ApplicationUserEntity>, IUserRepository
    {
        public UserRepository(SourceNameContext context) : base(context) { }

        public override void Delete(int id)
        {
            var entity = _context.Set<ApplicationUserEntity>().Single(u => u.Id == id);
            entity.IsActive = false;
            _context.SaveChanges();
        }

        public override ApplicationUserEntity GetById(int id)
        {
            return _context.Set<ApplicationUserEntity>()
                .Include(u => u.Roles)
                .Include("Roles.Role")
                .SingleOrDefault();
        }

        public ApplicationUserEntity GetByUsernameWithRoles(string username)
        {
            var entity = _context.Set<ApplicationUserEntity>()
                .Where(u => u.Username.ToLower() == username.ToLower())
                .Include(u => u.Roles)
                .Include("Roles.Role")
                .SingleOrDefault();

            return entity;
        }

        public override ApplicationUserEntity Update(ApplicationUserEntity inputUser)
        {
            var userEntity = _context.Set<ApplicationUserEntity>()
                .Include(u => u.Roles)
                .Single(u => u.Id == inputUser.Id);

            var rolesToRemove = userEntity.Roles.Where(
                existingRole =>
                !inputUser.Roles.Any(role => existingRole.ApplicationRoleId == role.ApplicationRoleId));
            foreach (var roleToRemove in rolesToRemove)
            {
                _context.Set<ApplicationUserRoleEntity>().Remove(roleToRemove);
            }

            var rolesToAdd = inputUser.Roles.Where(
                newRole =>
                !userEntity.Roles.Any(existingRole => existingRole.ApplicationRoleId == newRole.ApplicationRoleId));
            foreach (var roleToAdd in rolesToAdd)
            {
                userEntity.Roles.Add(new ApplicationUserRoleEntity
                {
                    ApplicationUserId = inputUser.Id,
                    ApplicationRoleId = roleToAdd.ApplicationRoleId
                });
            }

            userEntity.FirstName = inputUser.FirstName;
            userEntity.LastName = inputUser.LastName;
            userEntity.Username = inputUser.Username;

            _context.SaveChanges();
            return userEntity;
        }
    }
}