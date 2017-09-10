using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace zzbottom.helpers
{
    public static class Rigibody2DExtensions
    {
        public static List<RaycastHit2D> Cast(this Rigidbody2D @this, Vector2 direction)
        {
            var result = new List<RaycastHit2D>();
            var collisions = new RaycastHit2D[32];
            var count = @this.Cast(direction, collisions);
            for (var i = 0; i < count; i++)
                result.Add(collisions[i]);
            return result;
        }

        public static List<ContactPoint2D> GetContacts(this Rigidbody2D @this)
        {
            var result = new List<ContactPoint2D>();
            var collisions = new ContactPoint2D[32];
            var count = @this.GetContacts(collisions);
            for (var i = 0; i < count; i++)
                result.Add(collisions[i]);
            return result;
        }

        public static List<ContactPoint2D> GetContacts(this Rigidbody2D @this, ContactFilter2D filter)
        {
            var result = new List<ContactPoint2D>();
            var collisions = new ContactPoint2D[32];
            var count = @this.GetContacts(filter, collisions);
            for (var i = 0; i < count; i++)
                result.Add(collisions[i]);
            return result;
        }
    }
}
