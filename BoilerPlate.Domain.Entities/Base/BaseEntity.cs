using BoilerPlate.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Domain.Entities.Base
{
    public abstract class BaseEntity
    {


        public BaseEntity()
        {
            StatusEntity = StatusEntityEnum.Active;
            Created = DateTime.UtcNow;
        }

        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Column("Created", Order = 100)]
        public DateTime Created { get; set; }

        [Column("StatusEntity", Order = 101)]
        public StatusEntityEnum StatusEntity { get; set; }
        virtual public string StatusEntityName => StatusEntity.ToFriendlyString();
    }
}
