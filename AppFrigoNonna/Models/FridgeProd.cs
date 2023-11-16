using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppFrigoNonna.Models
{
    public class FridgeProd
    {
        [Key] public int Id { get; set; }

        // NOME
        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio")] // aggiunta per la VALIDAZIONE
        [MaxLength(50, ErrorMessage = "Lunghezza massima di 50 CARATTERI")] // aggiunta per la VALIDAZIONE
        public string Name { get; set; }

        
        
        // DESCRIZIONE
        [Column(TypeName = "text")]  // AGGIUNTA se si vogliono specificare ancora di più le colonne
        public string? Description { get; set; }


        
        // IMMAGINE URL
        [Url(ErrorMessage = "Devi inserire un link valido per l'immagine del prodotto")] // aggiunta per la VALIDAZIONE
        [MaxLength(500, ErrorMessage = "Il link non può essere più lungo di 500 caratteri")] // aggiunta per la VALIDAZIONE
        public string? ImgUrl { get; set; }



        // COLLOCAMENTO
        [Required(ErrorMessage ="Bisogna necessariamente specificare dove il prodotto va conservato")]
        public int Location { get; set; }

        // METODO PER NOME collocamento basato sul numero
        public string GetLocationName()
        {
            switch (Location)
            {
                case 1:
                    return "Frigo";
                case 2:
                    return "Freezer";
                case 3:
                    return "Dispensa";
                default:
                    return "Collocamento sconosciuto";
            }

            // ALTERNATIVA AL DEFAULT throw new InvalidOperationException("Collocamento sconosciuto") 
            // Differenza tra "InvalidOperationException" e semplicemente "Exception"?
        }



        // RELAZIONE N:N con le Categorie
        public List<Category>? Categories { get; set; }


        //======================================COSTRUTTORE========================================
        public FridgeProd(string name, string description, string imgUrl, int location)
        {
            this.Name = name;
            this.Description = description;
            this.ImgUrl = imgUrl;
            this.Location = location;
        }
        public FridgeProd() { }  // SEMPRE aggiungere costruttore VUOTO
    }
}
