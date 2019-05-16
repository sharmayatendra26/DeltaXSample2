using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Actor
{
    public interface IActorServices
    {
        decimal AddActor(ActorEntity Actor);
        ActorEntity GetActorById(int Id);
        List<ActorEntity> GetActorList();
        bool UpdateActor(int ActorId, ActorEntity ActorEntity);
    }
}
