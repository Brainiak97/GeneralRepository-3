using MetricService.BLL.DTO;
using MetricService.BLL.DTO.HealthMetricsBase;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.BLL.Mappers;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class HealthMetricsBaseService(IHealthMetricsBaseRepository healthMetricsBaseRepository,
        IValidator<HealthMetricsBase> validator, ClaimsPrincipal authorizationService) : IHealthMetricsBaseService
    {
        private readonly IHealthMetricsBaseRepository _repository = healthMetricsBaseRepository;
        private readonly IValidator<HealthMetricsBase> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;


        /// <summary>
        /// Удалить запись о базовых медицинских показателей
        /// </summary>
        /// <param name="healthMetricsBaseId">идентификатор записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о базовых мед. показателях не существует</exception>
        public async Task DeleteRecordOfHealthMetricsBaseAsync(int healthMetricsBaseId)
        {
            var healthMetricsBaseFind = await _repository.GetByIdAsync(healthMetricsBaseId) ??
               throw new IncorrectOrEmptyResultException("Указанная запись о базовых мед. показателях не существует", new Dictionary<object, object>()
               {
                    { "healthMetricsBaseId", healthMetricsBaseId }
               });

            if (!_authorizationService.IsInRole("Admin") && healthMetricsBaseFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись о базовых показателях здоровья", 
                    Common.Common.GetAuthorId(_authorizationService), healthMetricsBaseFind.UserId, _repository.Name);
            }

            await _repository.DeleteAsync(healthMetricsBaseId);
        }

        /// <summary>
        /// Получить все записи для пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">начало периода для выборки</param>
        /// <param name="endDate">конец периода для выборки</param>
        /// <param name="pageNum">номер страницы для пагинации</param>
        /// <param name="pageSize">кол-во строк на странице для пагинации</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public async Task<IEnumerable<HealthMetricsBaseDTO>> GetAllRecordsOfHealthMetricsBaseByUserIdAsync(RequestListWithPeriodByIdDTO requestListWithPeriodByIdDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && requestListWithPeriodByIdDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья",
                    Common.Common.GetAuthorId(_authorizationService), requestListWithPeriodByIdDTO.UserId, _repository.Name);
            }

            var recordsOfHealthMetricsBase = (await _repository.GetAllAsync()).Where(h => h.UserId == requestListWithPeriodByIdDTO.UserId && h.MetricDate >= requestListWithPeriodByIdDTO.BegDate &&
            h.MetricDate <= requestListWithPeriodByIdDTO.EndDate)
                .Skip((requestListWithPeriodByIdDTO.NumPage - 1) * requestListWithPeriodByIdDTO.PageSize).Take(requestListWithPeriodByIdDTO.PageSize).ToHealthMetricsBaseDTO();


            return recordsOfHealthMetricsBase;
        }

        /// <summary>
        /// получить запись о базовых показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId">ИД записи</param>
        /// <returns>модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Указанная запись о базовых мед. показателях не существует</exception>
        public async Task<HealthMetricsBaseDTO> GetRecordOfHealthMetricsBaseByIdAsync(int healthMetricsBaseId)
        {
            var healthMetricsBaseFind = await _repository.GetByIdAsync(healthMetricsBaseId) ??
                throw new IncorrectOrEmptyResultException("Указанная запись о базовых мед. показателях не существует", new Dictionary<object, object>()
                {
                    { "healthMetricsBaseId", healthMetricsBaseId }
                });

            if (!_authorizationService.IsInRole("Admin") && healthMetricsBaseFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья", 
                    Common.Common.GetAuthorId(_authorizationService), healthMetricsBaseFind.UserId, _repository.Name);
            }

            return healthMetricsBaseFind.ToHealthMetricsBaseDTO();
        }

        /// <summary>
        /// обновить запись
        /// </summary>
        /// <param name="healthMetricsBaseDTO">информвция о записи</param>        
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        /// <exception cref="IncorrectOrEmptyResultException">Запись о базовых мед. показателях не зарегистрирована</exception>
        public async Task UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseUpdateDTO healthMetricsBaseDTO)
        {
            var findHealthMetricsBase = await _repository.GetByIdAsync(healthMetricsBaseDTO.Id) ??
                throw new IncorrectOrEmptyResultException("Запись о базовых мед. показателях не зарегистрирована",
                    new Dictionary<object, object>()
                    {
                        {"healthMetricsBaseDTO", healthMetricsBaseDTO}
                    });

            if (!_authorizationService.IsInRole("Admin") && findHealthMetricsBase.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о базовых показателях здоровья для других пользователей", 
                    Common.Common.GetAuthorId(_authorizationService), findHealthMetricsBase.UserId, _repository.Name);
            }

            findHealthMetricsBase = healthMetricsBaseDTO.ToHealthMetricsBase(findHealthMetricsBase.UserId);

            if (!_validator.Validate(findHealthMetricsBase, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о базовых показателях здоровья пользователя", errorList);
            }

            await _repository.UpdateAsync(findHealthMetricsBase);
        }


        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="healthMetricsBaseDTO">Информация о записи</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        ///  <exception cref="IncorrectOrEmptyResultException">Запись о базовых мед. показателях уже зарегистрирована</exception>
        public async Task CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseCreateDTO healthMetricsBaseDTO)
        {

            if (!_authorizationService.IsInRole("Admin") && healthMetricsBaseDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о базовых показателях здоровья для других пользователей",
                    Common.Common.GetAuthorId(_authorizationService), healthMetricsBaseDTO.UserId, _repository.Name);
            }

            HealthMetricsBase healthMetricsBase = healthMetricsBaseDTO.ToHealthMetricsBase();

            if (!_validator.Validate(healthMetricsBase, out Dictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о базовых показателях здоровья пользователя", errorList);
            }

            await _repository.CreateAsync(healthMetricsBase);

        }
    }
}
