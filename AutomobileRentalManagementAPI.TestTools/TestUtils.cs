using Newtonsoft.Json;

namespace AutomobileRentalManagementAPI.TestTools
{
    public static class TestUtils
    {
        public static T2? CustomConvert<T1, T2>(T1 model)
        {
            string serializedModel = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<T2>(serializedModel);
        }
    }
}