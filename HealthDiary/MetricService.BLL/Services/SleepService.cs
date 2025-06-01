using MetricService.BLL.Dto;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;

namespace MetricService.BLL.Services
{
    public class SleepService(ISleepRepository sleepRepository, IValidator<Sleep> validator, ClaimsPrincipal authorizationService) : ISleepService
    {
        private readonly ISleepRepository _repository = sleepRepository;
        private readonly IValidator<Sleep> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService =authorizationService;


        /// <summary>
        /// Удаление информации о сне пользователя
        /// </summary>
        /// <param name="sleepId">ИД сна</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public async Task<bool> DeleteRecordOfSleepAsync(int sleepId)
        {
            var sleepFind = await _repository.GetByIdAsync(sleepId);
            if (sleepFind == null) return false;

            if (!_authorizationService.IsInRole("Admin") && sleepFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись о сне", Common.Common.GetAuthorId(_authorizationService), sleepFind.UserId, _repository.Name);
            }

            return await _repository.DeleteAsync(sleepId);
        }



        /// <summary>
        /// Получить все записи о снах пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">Начало периода для отбора значений</param>
        /// <param name="endDate">Конец периода для отбора значений</param>
        /// <param name="pageNum">Номер страницы для пагинации</param>
        /// <param name="pageSize">Кол-во строк на странице для пагинации</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException"></exception>
        public async Task<IEnumerable<SleepDTO>> GetAllRecordsOfSleepByUserIdAsync(int userId, DateTime begDate, DateTime endDate, int pageNum, int pageSize)
        {
            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о сне", Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            var sleeps = (await _repository.GetAllAsync()).Where(s => s.UserId == userId && s.StartSleep >= begDate && s.StartSleep <= endDate)
                .Skip((pageNum - 1) * pageSize).Take(pageSize);

            var sleepsDTO = new List<SleepDTO>();
            if (sleeps.Any())
            {
                foreach (var sleep in sleeps)
                {
                    sleepsDTO.Add(CreateSleepDTO(sleep)!);
                }
            }
            return sleepsDTO;
        }



        /// <summary>
        /// Получить информацию о сне
        /// </summary>
        /// <param name="sleepId">ИД сна</param>
        /// <returns>Модель DTO</returns>
        /// <exception cref="ViolationAccessException"></exception>
        public async Task<SleepDTO?> GetRecordOfSleepByIdAsync(int sleepId)
        {
            var sleepFind = await _repository.GetByIdAsync(sleepId);
            if (sleepFind == null) return null;

            if (!_authorizationService.IsInRole("Admin") && sleepFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о сне", Common.Common.GetAuthorId(_authorizationService), sleepFind.UserId, _repository.Name);
            }

            return CreateSleepDTO(sleepFind);
        }



        /// <summary>
        /// Создание информации о сне
        /// </summary>
        /// <param name="sleepDTO">Сон</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> CreateRecordOfSleepAsync(SleepDTO sleepDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && (sleepDTO.UserId != Common.Common.GetAuthorId(_authorizationService)))
            {
                throw new ViolationAccessException("Вы не можете создавать данные о сне для других пользователей", Common.Common.GetAuthorId(_authorizationService), sleepDTO.UserId, _repository.Name);
            }

            Sleep sleep = CreateModelFromDTO(sleepDTO);

            if (!_validator.Validate(sleep, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о сне пользователя", errorList);
            }

            return await _repository.CreateAsync(sleep);
        }


        /// <summary>
        /// Обновление информации о сне
        /// </summary>
        /// <param name="sleepDTO">сон</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> UpdateRecordOfSleepAsync(SleepDTO sleepDTO)
        {
            var findSleep = await _repository.GetByIdAsync(sleepDTO.Id);
            if (findSleep == null) return false;

            if (!_authorizationService.IsInRole("Admin") && (findSleep.UserId != Common.Common.GetAuthorId(_authorizationService)))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о сне для других пользователей", Common.Common.GetAuthorId(_authorizationService), findSleep.UserId, _repository.Name);
            }

            findSleep.StartSleep = sleepDTO.StartSleep;
            findSleep.EndSleep = sleepDTO.EndSleep;
            findSleep.Comment = sleepDTO.Comment;

            if (!_validator.Validate(findSleep, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о сне пользователя", errorList);
            }
                        
            return await _repository.UpdateAsync(findSleep);
        }

        /// <summary>
        /// Создание SleepDTO по модели "Сон"
        /// </summary>
        /// <param name="sleep">Модель</param>
        /// <returns>Модель DTO</returns>
        private static SleepDTO? CreateSleepDTO(Sleep? sleep)
        {
            if (sleep == null) return null;
            return new SleepDTO
            {
                Id = sleep.Id,
                Comment = sleep.Comment,
                EndSleep = sleep.EndSleep,
                QualityRating = sleep.QualityRating,
                StartSleep = sleep.StartSleep,
                UserId = sleep.UserId,


            };
        }



        /// <summary>
        /// Создание модели из DTO
        /// </summary>
        /// <param name="sleepDTO">Модель DTO</param>
        /// <returns>Модель</returns>
        private static Sleep CreateModelFromDTO(SleepDTO sleepDTO)
        {
            var sleep = new Sleep
            {
                Id = sleepDTO.Id,
                Comment = sleepDTO.Comment,
                EndSleep = sleepDTO.EndSleep,
                QualityRating = sleepDTO.QualityRating,
                StartSleep = sleepDTO.StartSleep,
                UserId = sleepDTO.UserId

            };
            return sleep;
        }
    }
}
