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

namespace BusinessServices.Actor
{
    public class ActorServices : IActorServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActorServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public decimal AddActor(ActorEntity Actor)
        {
            decimal result = 0;
            bool isExist = _unitOfWork.ActorRepository.IsExist(Actor.ActorId);
            if (!isExist)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DM.Actor ActorDetail = new DM.Actor
                    {
                        ActorId = Actor.ActorId,
                        Bio = Actor.Bio,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        DOB = Actor.DOB,
                        IsDeleted = Actor.IsDeleted,
                        Name = Actor.Name,
                        Sex = Actor.Sex,
                        ModifiedBy = string.Empty,
                        CreatedBy = string.Empty
                    };
                    _unitOfWork.ActorRepository.Insert(ActorDetail);
                    _unitOfWork.Save();
                    scope.Complete();
                    result = ActorDetail.ActorId;
                }
            }
            return result;
        }

        public bool UpdateActor(int ActorId, ActorEntity ActorEntity)
        {
            bool success = false;
            if (ActorEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var Actor = _unitOfWork.ActorRepository.GetByID(ActorEntity.ActorId);
                    if (Actor != null)
                    {
                        Actor.Name = ActorEntity.Name;
                        Actor.Bio = ActorEntity.Bio;                        
                        Actor.ModifiedOn = DateTime.Now;
                        Actor.DOB = ActorEntity.DOB;
                        Actor.Sex = ActorEntity.Sex;
                        _unitOfWork.ActorRepository.Update(Actor);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DisContinueActor(int ActorId)
        {
            bool success = false;
            if (ActorId > 0)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DM.Actor Actor = _unitOfWork.ActorRepository.GetByID(ActorId);
                    if (ActorId > 0)
                    {
                        Actor.IsDeleted = true;
                        _unitOfWork.ActorRepository.Update(Actor);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public ActorEntity GetActorById(int id)
        {
            DM.Actor Actor = _unitOfWork.ActorRepository.GetByID(id);
            if (Actor != null)
            {
                Mapper.CreateMap<DM.Actor, ActorEntity>();
                var ActorModel = Mapper.Map<DM.Actor, ActorEntity>(Actor);
                return ActorModel;
            }
            return null;
        }

        public List<ActorEntity> GetActorList()
        {
            try
            {
                var actors = _unitOfWork.ActorRepository.GetAll().ToList();
                if (actors.Any())
                {
                    Mapper.CreateMap<DM.Actor, ActorEntity>();
                    var actorsModel = Mapper.Map<List<DM.Actor>, List<ActorEntity>>(actors);
                    return actorsModel;
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
            
            return null;
        }
    }
}
