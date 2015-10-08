using System;
using System.Collections.Generic;
using System.Text;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Kod formy terenu
    /// </summary>
    public class KodTerenu
    {
        public static bool IsDroga(string kod)
        {
            switch (kod)
            {
                case "T09":
                case "T10":
                case "T11":
                case "T12":
                    break;
                default:
                    return false;
            }

            return true;
        }

        public static bool IsKoryto(string kod)
        {
            switch (kod)
            {
                case "K01":
                case "K02":
                case "K03":
                case "K04":
                case "K05":
                    break;
                default:
                    return false;
            }

            return true;
        }

        public static bool IsValid(string kod)
        {
            switch (kod)
            {
                case "":
                case "K01": //ziemia, muł
                case "K02": //Piasek
                case "K03": //drobny żwir
                case "K04": //gruby żwir
                case "K05": //Kamienie
                case "T01": //Trawa - niska
                case "T02": //Trawa - wysoka
                case "T03": //uprawy zbożowe
                case "T04": //las - rzadki
                case "T05": //las - gęsty
                case "T06": //zakrzaczenia - niskie
                case "T07": //zakrzaczenia - wysokie
                case "T08": //nieużytki
                case "T09": //drogi - asfalt
                case "T10": //drogi - beton
                case "T11": //drogi - tłuczeń
                case "T12": //drogi - droga gruntowa
                case "T13": //obiekty kabutarowe (zabudowa) - niska
                case "T14": //obiekty kabutarowe (zabudowa) - wysoka
                    break;
                default:
                    return false;
            }

            return true;
        }

        public static string OpisKodu(string kod)
        {
            switch (kod)
            {
                case "": return "pierwsza pikieta";
                case "K01": return "koryto - ziemia, muł";
                case "K02": return "koryto - Piasek";
                case "K03": return "koryto - drobny żwir";
                case "K04": return "koryto - gruby żwir";
                case "K05": return "koryto - Kamienie";
                case "T01": return "Trawa - niska";
                case "T02": return "Trawa - wysoka";
                case "T03": return "uprawy zbożowe";
                case "T04": return "las - rzadki";
                case "T05": return "las - gęsty";
                case "T06": return "zakrzaczenia - niskie";
                case "T07": return "zakrzaczenia - wysokie";
                case "T08": return "nieużytki";
                case "T09": return "drogi - asfalt";
                case "T10": return "drogi - beton";
                case "T11": return "drogi - tłuczeń";
                case "T12": return "drogi - droga gruntowa";
                case "T13": return "obiekty kabutarowe (zabudowa) - niska";
                case "T14": return "obiekty kabutarowe (zabudowa) - wysoka";
                default:
                    break;
            }

            return "zły kod terenu";
        }

        /// <summary>
        /// Podaj opis drogi na podstawie kodu
        /// </summary>
        /// <param name="kod"></param>
        /// <returns></returns>
        public static string OpisDrogi(string kod)
        {
            switch (kod)
            {
                case "T09": return "droga - asfalt";
                case "T10": return "droga - beton";
                case "T11": return "droga - tłuczeń";
                case "T12": return "droga gruntowa";
                default:
                    break;
            }

            return "to nie jest droga";
        }
    }

    public enum KodFormyTerenu
    {
        Pierwszy,
        K01,
        K02,
        K03,
        K04,
        K05,
        T01,
        T02,
        T03,
        T04,
        T05,
        T06,
        T07,
        T08,
        T09,
        T10,
        T11,
        T12,
        T13,
        T14,
        Invalid
    }
}
