using System;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string message) : base(message) { }
    }
}
