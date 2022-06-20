using SalesProjectMVC.Data;
using SalesProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesProjectMVC.Services.Exceptions;

namespace SalesProjectMVC.Services
{
    public class SellerService
    {
        private readonly SalesProjectMVCContext _context;

        public SellerService(SalesProjectMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(seller => seller.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();

        }

        public void Update(Seller seller)
        {
            if(!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Seller not found");
            }

            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
