using AutoMapper;
using BusinessEntities;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DM = DataModel;

namespace BusinessServices.Producer
{
    public class ProducerServices: IProducerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProducerServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public decimal AddProducer(ProducerEntity Producer)
        {
            decimal result = 0;
            bool isExist = _unitOfWork.ProducerRepository.IsExist(Producer.ProducerId);
            if (!isExist)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DM.Producer ProducerDetail = new DM.Producer
                    {
                        ProducerId = Producer.ProducerId,
                        Bio = Producer.Bio,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        DOB = Producer.DOB,
                        IsDeleted = Producer.IsDeleted,
                        Name = Producer.Name,
                        Sex = Producer.Sex,
                        ModifiedBy = string.Empty,
                        CreatedBy = string.Empty
                    };
                    _unitOfWork.ProducerRepository.Insert(ProducerDetail);
                    _unitOfWork.Save();
                    scope.Complete();
                    result = ProducerDetail.ProducerId;
                }
            }
            return result;
        }

        public bool UpdateProducer(int ProducerId, ProducerEntity ProducerEntity)
        {
            bool success = false;
            if (ProducerEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var Producer = _unitOfWork.ProducerRepository.GetByID(ProducerEntity.ProducerId);
                    if (Producer != null)
                    {
                        Producer.Name = ProducerEntity.Name;
                        Producer.Bio = ProducerEntity.Bio;
                        Producer.ModifiedOn = DateTime.Now;
                        Producer.DOB = ProducerEntity.DOB;
                        Producer.Sex = ProducerEntity.Sex;
                        _unitOfWork.ProducerRepository.Update(Producer);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DisContinueProducer(int ProducerId)
        {
            bool success = false;
            if (ProducerId > 0)
            {
                using (TransactionScope scope=new TransactionScope())
                {
                    DM.Producer Producer = _unitOfWork.ProducerRepository.GetByID(ProducerId);
                    if (ProducerId > 0)
                    {
                        Producer.IsDeleted = true;
                        _unitOfWork.ProducerRepository.Update(Producer);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }                
            }
            return success;
        }

        public ProducerEntity GetProducerById(int id)
        {
            DM.Producer Producer = _unitOfWork.ProducerRepository.GetByID(id);
            if (Producer != null)
            {
                Mapper.CreateMap<DM.Producer, ProducerEntity>();
                var ProducerModel = Mapper.Map<DM.Producer, ProducerEntity>(Producer);
                return ProducerModel;
            }
            return null;
        }

        public List<ProducerEntity> GetProducerList()
        {
            var Producers = _unitOfWork.ProducerRepository.GetAll().ToList();
            if (Producers.Any())
            {
                Mapper.CreateMap<DM.Producer, ProducerEntity>();
                var ProducersModel = Mapper.Map<List<DM.Producer>, List<ProducerEntity>>(Producers);
                return ProducersModel;
            }
            return null;
        }
    }
}
