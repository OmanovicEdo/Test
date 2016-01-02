using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class ConversionException: Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public ConversionException()
        {
            // Add implementation.
        }
        public ConversionException(string message)
        {
            // Add implementation.
        }
        public ConversionException(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected ConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");            
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
