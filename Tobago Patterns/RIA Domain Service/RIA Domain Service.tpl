using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using Adf.Core;
using $Class.Model$.Server.Business;
using $Class.Model$.Server.Web.Models;

namespace $Class.Model$.Server.Web.Services
{
    [EnableClientAccess]
    public partial class $Class.Name.Pascal$DomainService : DomainService
    {
        public IQueryable<$Class.Name.Pascal$Dto> Get$Class.Name.Pascal.Plural$()
        {
            return $Class.Name.Pascal$Factory.GetAll().Select($Class.Name.Camel$ =>
                                                   {
                                                       var dto = new $Class.Name.Pascal$Dto();

                                                       DomainMapper.Persist($Class.Name.Camel$, dto);

                                                       return dto;
                                                   })
                                                   .ToList().AsQueryable();
        }

        [Invoke]
        public $Class.Name.Pascal$Dto Get$Class.Name.Pascal$(string id)
        {
            var $Class.Name.Camel$ = $Class.Name.Pascal$Factory.Get(new ID(id));

            return DomainMapper.Persist($Class.Name.Camel$, new $Class.Name.Pascal$Dto());
        }

        [Invoke]
        public $Class.Name.Pascal$Dto Add$Class.Name.Pascal$($Class.Name.Pascal$Dto $Class.Name.Camel$Dto)
        {
            var $Class.Name.Camel$ = $Class.Name.Pascal$Factory.New();

            DomainMapper.Bind($Class.Name.Camel$, $Class.Name.Camel$Dto);

            $Class.Name.Pascal$Factory.Save($Class.Name.Camel$);

            var dto = new $Class.Name.Pascal$Dto();
            
            return DomainMapper.Persist($Class.Name.Camel$, dto);
        }

        [Invoke]
        public void Update$Class.Name.Pascal$($Class.Name.Pascal$Dto $Class.Name.Camel$)
        {
            var original = $Class.Name.Pascal$Factory.Get(new ID($Class.Name.Camel$.Id));

            DomainMapper.Bind(original, $Class.Name.Camel$);

            $Class.Name.Pascal$Factory.Save(original);
        }

        [Invoke]
        public bool Delete$Class.Name.Pascal$($Class.Name.Pascal$Dto $Class.Name.Camel$)
        {
            return $Class.Name.Pascal$Factory.Remove($Class.Name.Pascal$Factory.Get(new ID($Class.Name.Camel$.Id)));
        }

        // enable editable entity set
        [Insert] public void A($Class.Name.Pascal$Dto $Class.Name.Camel$) {}

        [Update] public void B($Class.Name.Pascal$Dto $Class.Name.Camel$) {}

        [Delete] public void C($Class.Name.Pascal$Dto $Class.Name.Camel$) {}
    }
}