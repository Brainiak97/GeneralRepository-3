using MetricService.BLL.DTO;
using MetricService.BLL.DTO.Regimen;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class RegimenService(IRegimenRepository regimenRepository, IValidator<Regimen> validator, ClaimsPrincipal authorizationService) : IRegimenService
    {
        private readonly IRegimenRepository _repository = regimenRepository;
        private readonly IValidator<Regimen> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;


        
        public async Task CreateRegimenAsync(RegimenCreateDTO regimenCreateDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && regimenCreateDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные для других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService),
                    regimenCreateDTO.UserId, 
                    _repository.Name);
            }

            Regimen regimen = regimenCreateDTO.ToRegimen();

            if (!_validator.Validate(regimen, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о схеме приема лекарств", errorList);
            }

            await _repository.CreateAsync(regimen);
        }

        
        public async Task DeleteRegimenAsync(int regimenId)
        {
            var regimenFind = await _repository.GetByIdAsync(regimenId) ??
                 throw new IncorrectOrEmptyResultException("Указанная схема приема не существует", new Dictionary<object, object>()
                 {
                    { "regimenId", regimenId }
                 });

            if (!_authorizationService.IsInRole("Admin") && regimenFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою схему приема", 
                    Common.Common.GetAuthorId(_authorizationService),
                    regimenFind.UserId, 
                    _repository.Name);
            }

            await _repository.DeleteAsync(regimenId);
        }

        
        public async Task<IEnumerable<RegimenDTO>> GetAllRegimenByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && 
                requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои схемы приема",
                    Common.Common.GetAuthorId(_authorizationService), 
                    requestListWithPeriodByIdDTO.UserId, 
                    _repository.Name);
            }

            var regimens = (await _repository.GetAllAsync())
                .Where(r => r.UserId == requestListWithPeriodByIdDTO.UserId &&
                                    r.StartDate >= requestListWithPeriodByIdDTO.BegDate &&
                                    r.StartDate <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize)
                .Take(requestListWithPeriodByIdDTO.PageSize).ToRegimenDTO();

            return regimens;
        }

        
        public async Task<RegimenDTO> GetRegimenByIdAsync(int regimenId)
        {
            var regimenFind = await _repository.GetByIdAsync(regimenId) ??
                throw new IncorrectOrEmptyResultException("Указанная схема приема не существует", new Dictionary<object, object>()
                {
                    { "regimenId", regimenId }
                });

            if (!_authorizationService.IsInRole("Admin") && regimenFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свою тренировку", 
                    Common.Common.GetAuthorId(_authorizationService),
                    regimenFind.UserId, 
                    _repository.Name);
            }

            return regimenFind.ToRegimenDTO();
        }

        
        public async Task UpdateRegimenAsync(RegimenUpdateDTO regimenUpdateDTO)
        {
            var regimenFind = await _repository.GetByIdAsync(regimenUpdateDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Схема приема не зарегистрирована",
                    new Dictionary<object, object>()
                    {
                        {"regimenUpdateDTO", regimenUpdateDTO}
                    });

            if (!_authorizationService.IsInRole("Admin") && regimenFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о тренировке для других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService),
                    regimenFind.UserId, 
                    _repository.Name);
            }

            regimenFind = regimenUpdateDTO.ToRegimen(regimenFind.UserId, regimenFind.MedicationId);

            if (!_validator.Validate(regimenFind, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о схеме приема", errorList);
            }

            await _repository.UpdateAsync(regimenFind);
        }
    }
}
