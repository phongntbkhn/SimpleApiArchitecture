namespace Simple.API.Models
{
    public interface IResponseModel
    {
    }

    public class BaseResultModel : IResponseModel
    {
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; }
        public BaseResultModel() { }

        public BaseResultModel(bool success, string message)
        {
            Message = message;
            IsSuccess = success;
        }

        #region Success
        public static BaseResultModel Success(string message)
        {
            return new BaseResultModel(true, message);
        }

        public static BaseResultModel Success(object? data)
        {
            return new BaseResultModel { IsSuccess = true, Data = data };
        }

        public static BaseResultModel Success(string message, object? data)
        {
            return new BaseResultModel { IsSuccess = true, Message = message, Data = data };
        }
        #endregion

        #region Failed
        public static BaseResultModel Fail(string message)
        {
            return new BaseResultModel(false, message);
        }
        #endregion
    }
}
