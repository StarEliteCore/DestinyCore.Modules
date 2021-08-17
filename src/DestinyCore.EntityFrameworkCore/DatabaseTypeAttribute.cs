
using DestinyCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.EntityFrameworkCore
{

    [AttributeUsage(AttributeTargets.Class)]
    public   class DatabaseTypeAttribute: Attribute
    {
        public DatabaseTypeAttribute(DataBaseType databaseType)
        {

            DatabaseType = databaseType;


        }

        public DataBaseType DatabaseType { get; private set; }
    }
}
