using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data {
    public class PlatformRepo : IPlatformRepo {
        private readonly AppDbContext _context;

        public PlatformRepo( AppDbContext context ){
            _context = context;
        }
        public void CreatePlatform( Platform platform ){
            if( platform == null ){
                throw new ArgumentNullException( nameof( platform ) );
            }

            _context.Platforms.Add( platform );
        }

        public Platform GetPlatformById( int id ){
            return _context.Platforms.FirstOrDefault( p => p.ID == id );
        }

        public IEnumerable<Platform> GetPlatforms(){
            return _context.Platforms.ToList();
        }

        public bool SaveChanges(){
            return ( _context.SaveChanges() >= 0 );
        }
    }
}