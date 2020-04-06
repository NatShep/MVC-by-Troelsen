using System;
using System.ComponentModel.DataAnnotations;

namespace AutoLotDal.Models.Base
{
    public class EntityBase
    {
        [Key] public int Id { set; get; }
        [Timestamp] public byte[] Timestamp { get; set; }
    }
}