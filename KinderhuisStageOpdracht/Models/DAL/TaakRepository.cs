﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KinderhuisStageOpdracht.Models.Domain;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class TaakRepository:ITaakRepository
    {
        private readonly ProjectContext _context;

        public TaakRepository(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Taak> FindAll()
        {
            return _context.TaakSet.OrderBy(t => t.Id);
        }

        public Taak FindById(int id)
        {
            return _context.TaakSet.Find(id);
        }

        public void AddMenu(Taak taak)
        {
            _context.TaakSet.Add(taak);
        }

        public void DeleteMenu(int id)
        {
            Taak taak = _context.TaakSet.Find(id);
            _context.TaakSet.Remove(taak);
        }

        public void UpdateMenu(Taak taak)
        {
            _context.Entry(taak).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}