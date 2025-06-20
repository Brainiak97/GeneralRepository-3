﻿using MetricService.BLL.DTO;

namespace MetricService.BLL.Exceptions
{
    public abstract class BaseException : ApplicationException
    {
        protected BaseException(string message, Dictionary<object, object>? errorDetail = null) : base(message)
        {
            if (errorDetail != null)
            {
                foreach (var item in errorDetail)
                {
                    Data.Add(item.Key, item.Value);
                }
            }
        }

        public virtual ErrorDTO GetError()
        {
            return new ErrorDTO()
            {
                Message = this.Message,
                DetailErrors = Data
            };
        }
    }
}
