using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using SourceName.Data.Model.Roles;
using SourceName.Data.Model.Users;
using SourceName.Data.Users;
using SourceName.Service.Model.Users;
using SourceName.Service.Users;

namespace SourceName.Service.Implementation.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserPasswordService _userPasswordService;
        private readonly IUserRepository _userRepository;

        public UserService(
            IMapper mapper,
            IUserPasswordService userPasswordService,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;
            _userPasswordService = userPasswordService;
            _userRepository = userRepository;
        }

        public ApplicationUser CreateUser(ApplicationUser user)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            _userPasswordService.CreateHash(user.Password, out passwordHash, out passwordSalt);

            var userEntity = new ApplicationUserEntity
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true
            };

            userEntity.Roles = user.Roles.Select(r =>
                new ApplicationUserRoleEntity
                {
                    ApplicationUserId = userEntity.Id,
                    ApplicationRoleId = r.ApplicationRoleId
                }).ToList();

            var result = _userRepository.Insert(userEntity);
            return _mapper.Map<ApplicationUser>(result);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public List<ApplicationUser> GetAll()
        {
            var userEntities = _userRepository.Get()
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName);

            return _mapper.Map<List<ApplicationUser>>(userEntities);
        }

        public ApplicationUser GetById(int id)
        {
            var userEntity = _userRepository.GetById(id);
            return _mapper.Map<ApplicationUser>(userEntity);
        }

        public ApplicationUser GetByUsername(string username)
        {
            var userEntity = _userRepository.Get(x => x.Username == username).SingleOrDefault();
            return _mapper.Map<ApplicationUser>(userEntity);
        }

        public ApplicationUser GetForAuthentication(string username)
        {
            var userEntity = _userRepository.GetByUsernameWithRoles(username);
            return _mapper.Map<ApplicationUser>(userEntity);
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            var userEntity = _mapper.Map<ApplicationUserEntity>(user);
            userEntity.Roles = user.Roles.Select(role => new ApplicationUserRoleEntity
            {
                ApplicationUserId = user.Id,
                ApplicationRoleId = role.ApplicationRoleId
            }).ToList();

            return _mapper.Map<ApplicationUser>(_userRepository.Update(userEntity));
        }
    }
}