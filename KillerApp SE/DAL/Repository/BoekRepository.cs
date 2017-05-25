using System.Collections.Generic;
using KillerApp_SE.SQLContext;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLRepository
{
    public class BoekRepository
    {
        BoekPersistency bpers = new BoekPersistency();

        public List<Boek> ZoekBoek(string titel)
        {
            return bpers.ZoekBoek(titel);
        }
        public List<Boek> GetBoekenlijst()
        {
            return bpers.GetBoekenlijst();
        }
        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            return bpers.GetMijnBoeken(gebruikernaam);
        }
        public void LeenBoek(string gebruikernaam, string titel)
        {
            bpers.LeenBoek(gebruikernaam, titel);
        }
        public void RetourBoek(string gebruikernaam, string titel)
        {
            bpers.RetourBoek(gebruikernaam, titel);
        }
        public void UpdateBoek(string titel)
        {
            bpers.UpdateBoek(titel);
        }
    }
}