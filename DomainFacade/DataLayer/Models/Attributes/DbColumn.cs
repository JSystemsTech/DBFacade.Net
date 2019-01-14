using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainFacade.DataLayer.Models.Attributes
{
    public class DbColumn : Attribute
    {

        private string name;
        private object defaultValue;
        private char delimeter = ',';
        private string dateFormat;
        private bool isDateFormat;

        public DbColumn(string name)
        {
            this.name = name;
            this.defaultValue = null;
        }
        public DbColumn(string name, object defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }
        public DbColumn(string name, char delimeter)
        {
            this.name = name;
            this.defaultValue = null;
            this.delimeter = delimeter;
        }
        public DbColumn(string name, string dateFormat, bool isDateFormat)
        {
            this.name = name;
            this.defaultValue = null;
            this.dateFormat = dateFormat;
            this.isDateFormat = isDateFormat;
            if (!isDateFormat)
            {
                defaultValue = dateFormat;
            }
        }
        public DbColumn(string name, string defaultValue, string dateFormat)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            this.dateFormat = dateFormat;
            this.isDateFormat = true;
        }
        public DbColumn(string name, object defaultValue, char delimeter)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            this.delimeter = delimeter;
        }

        public bool HasColumn(IDataRecord data)
        {
            try
            {
                return GetOrdinal(data) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        public int GetOrdinal(IDataRecord data)
        {
            return data.GetOrdinal(this.Name);
        }


        public virtual string Name
        {
            get { return name; }
        }
        public virtual object DefaultValue
        {
            get { return defaultValue; }
        }
        public virtual char Delimeter
        {
            get { return delimeter; }
        }
        public virtual string DateFormat
        {
            get { return dateFormat; }
        }
        public virtual bool IsDateFormat
        {
            get { return isDateFormat; }
        }
    }
}
