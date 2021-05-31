using AbdrakhmanovPCConfigurator.WPF.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdrakhmanovPCConfigurator.WPF
{
    public partial class FinishedPC
    {
        public int GetPriceFinishedPC
        {
            get
            {
                int price = 0;
                foreach (var component in GetListComponents)
                {
                    price += component.GetPriceComponent;
                }
                return price;
            }
        }

        public List<Component> GetListComponents
        {
            get
            {
                List<int> componentsId = GetListComponentsId;
                List<Component> componentsObject = new List<Component>();
                var componentsDB = GetItemObject.DB.Components;

                foreach (var item in componentsId)
                {
                    var componentFind = componentsDB.FirstOrDefault(obj => obj.Id == item);
                    if (componentFind != null)
                        componentsObject.Add(componentFind);
                }
                return componentsObject;
            }

        }

        public List<int> GetListComponentsId
        {
            get => JsonConvert.DeserializeObject<List<int>>(this.Components);
        }

        public List<int> SetComponents
        {
            set
            {
                Components = JsonConvert.SerializeObject(value);
            }
        }

        public bool ItsAdmin
        {
            get
            {
                if (GetItemObject.AuthrizationPerson.Type == "Администратор")
                    return true;
                else
                    return false;
            }
        }
        public bool ItsClient
        {
            get
            {
                if (GetItemObject.AuthrizationPerson.Type == "Клиент")
                    return true;
                else
                    return false;
            }
        }
    }
}
