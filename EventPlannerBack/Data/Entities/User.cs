using System.ComponentModel.DataAnnotations;

namespace EventPlannerBack.Data.Entities
{
    public class User
    {
        public int id { get; set; }
        [DataType("nvarchar(64)")]
        public string username { get; set; }
        [DataType("nvarchar(64)")]
        public string email { get; set; }
        [DataType("nvarchar(64)")]
        public string password { get; set; }
    }
}
