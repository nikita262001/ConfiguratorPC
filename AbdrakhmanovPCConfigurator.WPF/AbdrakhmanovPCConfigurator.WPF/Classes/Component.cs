using AbdrakhmanovPCConfigurator.WPF.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdrakhmanovPCConfigurator.WPF
{
    public partial class Component
    {
        private AbdrakhmanovAPCConfiguratorEntities db = GetItemObject.DB;
        public string GetFullName
        {
            get
            {
                bool next = true;
                int idParent = int.Parse(Parent_Id.ToString());
                string fullName = Name;
                var components = db.Components;
                while (next)
                {
                    var selectComponent = components.FirstOrDefault(obj => obj.Id == idParent);
                    if (selectComponent.Parent_Id == null && selectComponent.Parametrs == null)
                        next = false;
                    else
                    {
                        fullName = $"{selectComponent.Name} {fullName}";
                        idParent = int.Parse(selectComponent.Parent_Id.ToString());
                    }
                }
                return fullName;
            }
        }
        public Component GetRootParentComponent
        {
            get
            {
                bool next = true;
                Component parentComponent = this;
                var components = db.Components;
                while (next)
                {
                    var selectComponent = components.FirstOrDefault(obj => obj.Id == parentComponent.Parent_Id);
                    if (selectComponent.Parent_Id == null && selectComponent.Parametrs == null)
                        next = false;
                    parentComponent = selectComponent;
                }
                return parentComponent;
            }
        }

        public int GetPriceComponent
        {
            get => int.Parse(JsonConvert.DeserializeObject<List<Parametr>>(Parametrs).FirstOrDefault(obj => obj.Key == "Price").Value);
        }

        public List<Parametr> GetPrametrsComponent
        {
            get => JsonConvert.DeserializeObject<List<Parametr>>(Parametrs);
        }

        public void RemoveKascadeComponent()
        {
            var component = this;
            var componentsParent = db.Components.Where(obj => obj.Parent_Id == component.Id).ToList();
            if (componentsParent.Count != 0)
            {
                for (int i = 0; i < componentsParent.Count; i++)
                    componentsParent[i].RemoveKascadeComponent();
            }
            db.Components.Remove(component);
            db.SaveChanges();
        }
    }
}
