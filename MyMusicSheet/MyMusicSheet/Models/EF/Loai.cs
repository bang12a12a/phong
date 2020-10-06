namespace MyMusicSheet.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loai")]
    public partial class Loai
    {
        [StringLength(50)]
        public string Id { get; set; }

        public string TenLoai { get; set; }
    }
}
