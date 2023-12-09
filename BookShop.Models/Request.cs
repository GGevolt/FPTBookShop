using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FPTBookShop.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }
        public DateTime Request_Date { get; set; }
        public string Request_Name { get; set;}
        public string Request_Description { get; set;}

        public string Status { get; set;}

        public string UserId { get; set;}


    }
}
