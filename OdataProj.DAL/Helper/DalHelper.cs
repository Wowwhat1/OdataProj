using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataProj.DAL.Helper
{
    public class DalHelper
    {

        public static void DetachAllEntities(DbContext context)
        {
            var changedEntities = context.ChangeTracker.Entries().ToList();
            foreach (var entry in changedEntities)
            {
                entry.State = EntityState.Detached;
            }
        }

    }
}
