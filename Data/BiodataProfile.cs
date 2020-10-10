using AutoMapper;
using Newtonsoft.Json.Linq;

namespace CoreCodeCamp.Data
{
    public class BiodataProfile : Profile
    {
        public BiodataProfile()
        {
            this.CreateMap<JObject, BiodataModel>()
                .ForMember("Id", jData => { jData.MapFrom(jo => jo["biotoolsID"]); })
                .ForMember("Name", jData => { jData.MapFrom(jo => jo["name"]); })
            .ForMember("Description", jData => { jData.MapFrom(jo => jo["description"]); })
            .ForMember("Homepage", jData => { jData.MapFrom(jo => jo["homepage"]); });
        }
    }
}