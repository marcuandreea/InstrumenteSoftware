using System.Collections.Generic;
using mvc.Models;

namespace mvc.ViewModels
{
    public class CartViewModel
    {
        public List<Espressoare> Espressoare { get; set; }
        public List<Tipuri_Cafea> TipuriCafea { get; set; }
        public List<Cos_cumparaturi> CosCumparaturi { get; set; }
    }
}
