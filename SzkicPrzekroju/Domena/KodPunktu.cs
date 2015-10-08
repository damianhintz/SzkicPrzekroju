using System;
using System.Collections.Generic;
using System.Text;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Kod punktu
    /// </summary>
    public class KodPunktu
    {
        public static bool IsValid(string kodPunktu)
        {
            switch (kodPunktu)
            {
                case "1": //górna krawędź skarpy - lewy teren zalewowy
                case "7": //najniższy punkt dna koryta
                case "12": //górna krawędź skarpy - prawy teren zalewowy
                    break; //przekrój korytowy
                case "30": //terenowy punkt - suchy
                case "31": //dolna krawędź - lewa strona
                case "32": //górna krawędź - lewa strona
                case "33": //korona - środek
                case "34": //górna krawędź - prawa strona
                case "35": //dolna strona - prawa strona
                case "36": //górna krawędź mobilnego (ruchomego) urządzenia zabezpieczającego
                    break; //wały, zapory, mury
                case "40": //dolna krawędź budowli
                case "41": //górna krawędź budowli
                case "42": //górna krawędź barierki (także barierki ochronne itd..)
                case "43": //szczególne punkty budowli (np. zadaszenie porzejścia kładki)
                    break; //profil budowli
                case "50": //dolna krawędź korony budoli hydrotechnicznej
                case "51": //górna krawędź korony budoli hydrotechnicznej
                case "52": //dno rury, kanału
                case "53": //górna krawędź przykrywy kanałowej
                case "54": //dno kanału
                    break; //pojedyncze punkty budowli
                case "60": //ogólnie punkt okręgu
                    break; //przepusty, profile okrągłe
                case "100": //markery przepływu wysokiej wody
                case "101": //miejsca dokumentacji fotograficznej
                case "102": //sczególne punkty budowli (np. budynki mieszkalne góra/dół)
                case "103": //brzeg ulicy
                case "104": //górna krawędź torów
                case "ZWW": //zwierciadło wody
                    break; //szczególne punkty
                case "200": //punkt wykonania pomiaru
                case "201": //stabilizowane punkty osnowy pomiarowej
                case "202": //punkty graniczne
                case "203": //punkty osnowy szczegółowej
                case "204": //repery wysokościowe
                case "205": //oznaczony punkt profilu
                case "206": //punkt kierunkowy profilu - nieoznaczony
                    break; //punkty bazowe pomiarów
                default:
                    return false; //nieprawidłowy typ
            }

            return true;
        }
    }
}
