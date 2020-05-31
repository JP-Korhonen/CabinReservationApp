using CommonModels;
using System.Text;

namespace FrontEnd.Utility
{
    public class InvoicePdfGenerator
    {
        public static string GetHTMLString(Invoice invoiceData)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"
                <html>
                <head></head>
                <body>
                        <div class='div-container-right'>
                                <img src = 'https://cabinreservationsystemsa.blob.core.windows.net/cabinreservationsystemblob/vp3.png' height='150' /><br>
                                Village People Oy<br />
                                Mökkitie 1<br />
                                Evitskog<br />
                                02550 Kirkkonummi
                        </div>
                <h1>Lasku</h1>
   
                <br />
                <br />

 
                    
                           

                                <br /><br />
                                {0}<br />
                                {1} <br />
                                {2}<br />
                                {3}<br />
                                {4}<br />
                                {5}<br />
                                {6}
                             
                           

                
                        <br />", invoiceData.CabinReservation.Person.FirstName, invoiceData.CabinReservation.Person.LastName, invoiceData.CabinReservation.Person.Email, invoiceData.CabinReservation.Person.PhoneNumber,
                                 invoiceData.CabinReservation.Person.Address, invoiceData.CabinReservation.Person.Post.PostalCode, invoiceData.CabinReservation.Person.Post.City
                );
            sb.AppendFormat(@"
                   
                        <h3>Varaus</h3>
                        {0}<br />
                        {1}<br />
                        {2}<br />
                        {3}<br />
                        {4}<br />
                        Pinta-ala: {5} m²<br />
                        Makuuhuoneet: {6}<br />
                        Hinta: {7} €/vrk
                   
", invoiceData.CabinReservation.Cabin.Resort.ResortName, invoiceData.CabinReservation.Cabin.CabinName, invoiceData.CabinReservation.Cabin.Address, invoiceData.CabinReservation.Cabin.PostalCode,
                                          invoiceData.CabinReservation.Cabin.Post.City, invoiceData.CabinReservation.Cabin.Area, invoiceData.CabinReservation.Cabin.Rooms, invoiceData.CabinReservation.Cabin.CabinPricePerDay
                );

            sb.AppendFormat(@"
                  
                        <h3>Varauspäivä</h3>
                        {0}
                        <br />
                        <br />
                        <h3>Varauksen kesto</h3>
                        {1} - {2}<br /><br />
                   

                ", invoiceData.CabinReservation.ReservationBookingTime.ToString("dd.MM.yyy"), invoiceData.CabinReservation.ReservationStartDate.ToString("dd.MM.yyy"),
                        invoiceData.CabinReservation.ReservationEndDate.ToString("dd.MM.yyy")
                );

            sb.AppendFormat(@"

                        <br />
                        
                            <h2>Kokonaishinta {0} €</h2>
                      

                        </body>
                        </html>", invoiceData.InvoiceTotalAmount

                );

            return sb.ToString();
        }
    }
}


//@((invoiceData.CabinReservation.ReservationEndDate - invoiceData.CabinReservation.ReservationStartDate).TotalDays.ToString())
//            @{var i = (invoiceData.CabinReservation.ReservationEndDate - invoiceData.CabinReservation.ReservationStartDate).TotalDays;}
//            @(i == 1 ? 'yö' : 'y