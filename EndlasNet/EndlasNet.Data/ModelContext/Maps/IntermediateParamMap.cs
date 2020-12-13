using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class IntermediateParamMap
    {
        private Func<EntityTypeBuilder<IntermediateParam>> entity;

        public IntermediateParamMap(Func<EntityTypeBuilder<IntermediateParam>> entity)
        {
            this.entity = entity;
        }
    }
}
