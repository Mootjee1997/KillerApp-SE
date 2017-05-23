using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerApp_SE.SQLContext;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLRepository
{
    public class BoekRepository
    {
        SQLPersistency db = new SQLPersistency();

        public List<Boek> ZoekBoek(string titel)
        {
            return db.ZoekBoek(titel);
        }
        public List<Boek> GetBoekenlijst()
        {
            return db.GetBoekenlijst();
        }
        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            return db.GetMijnBoeken(gebruikernaam);
        }
        public void LeenBoek(string gebruikernaam, string titel)
        {
            db.LeenBoek(gebruikernaam, titel);
        }
        public void RetourBoek(string gebruikernaam, string titel)
        {
            db.RetourBoek(gebruikernaam, titel);
        }
        public void UpdateBoek(string titel)
        {
            db.UpdateBoek(titel);
        }
    }
}