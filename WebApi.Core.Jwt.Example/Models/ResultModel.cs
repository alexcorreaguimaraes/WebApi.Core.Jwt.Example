using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core.Jwt.Example.Models
{
    public class ErrorResultModel
    {
        Guid _returnCod = Guid.NewGuid();
        public Guid returnCod
        {
            get
            {
                return _returnCod;
            }
            set
            {
                _returnCod = value;
            }
        }
        public List<string> Messages { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }
    }

    public class ResultModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}