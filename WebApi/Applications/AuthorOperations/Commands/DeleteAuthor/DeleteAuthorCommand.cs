using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataBaseOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar Mevcut Değil, Silinemedi!");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
