using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Producer
{
    public interface IProducerServices
    {
        decimal AddProducer(ProducerEntity Actor);
        ProducerEntity GetProducerById(int Id);
        List<ProducerEntity> GetProducerList();
        bool UpdateProducer(int ProducerId, ProducerEntity ProducerEntity);
    }
}
