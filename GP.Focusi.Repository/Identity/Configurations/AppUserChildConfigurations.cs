using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Identity.Configurations
{
    public class AppUserChildConfigurations : IEntityTypeConfiguration<AppUserChild>
    {
        public void Configure(EntityTypeBuilder<AppUserChild> builder)
        {
            //builder.HasMany<ChildClass>()
            //	.WithOne()
            //	.HasForeignKey(c => c.Id);

            //builder.HasOne(AC => AC.ChildClass)
            //	.WithMany()
            //	.HasForeignKey(C => C.Id);
            //builder.OwnsOne(A => A.ChildClass, Cc => Cc.WithOwner());
        }
    }
}
