using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.Models
{
    [MetadataType(typeof(AuthorMetaData))]

    public partial class Author
    {
        public class AuthorMetaData
        {

            [DisplayName("Author Name")]
            public string AuthorName { get; set; }

            [DisplayName("Author ID")]
            public string AuthorId { get; set; }

        }


    }
    
}