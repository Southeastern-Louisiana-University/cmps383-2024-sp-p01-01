using System.Collections.Generic;

namespace Selu383.SP24.Api.Common
{
    public class Response
    {
        public object Data { get; set; } = string.Empty;
        public List<Error> Errors { get; set; } = new List<Error>();
        public bool HasErrors => Errors.Count > 0;

        public void AddError(string property, string message)
        {
            Errors.Add(new Error(property, message));
        }
    }
}
