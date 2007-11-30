using System;
using System.Collections;
using GeoAPI.Coordinates;
using GisSharpBlog.NetTopologySuite.Algorithm;
using GisSharpBlog.NetTopologySuite.GeometriesGraph;
using GisSharpBlog.NetTopologySuite.GeometriesGraph.Index;
using GisSharpBlog.NetTopologySuite.Operation.Relate;
using NPack.Interfaces;

namespace GisSharpBlog.NetTopologySuite.Operation.Valid
{
    /// <summary> 
    /// Checks that a {GeometryGraph} representing an area
    /// (a <see cref="Polygon{TCoordinate}" /> or <c>MultiPolygon</c> )
    /// is consistent with the SFS semantics for area geometries.
    /// Checks include:
    /// Testing for rings which self-intersect (both properly and at nodes).
    /// Testing for duplicate rings.
    /// If an inconsistency if found the location of the problem is recorded.
    /// </summary>
    public class ConsistentAreaTester<TCoordinate>
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, IComparable<TCoordinate>,
                            IComputable<TCoordinate>, IConvertible
    {
        private readonly LineIntersector<TCoordinate> _li = new RobustLineIntersector<TCoordinate>();
        private readonly GeometryGraph<TCoordinate> _geomGraph;
        private readonly RelateNodeGraph<TCoordinate> _nodeGraph = new RelateNodeGraph<TCoordinate>();

        // the intersection point found (if any)
        private ICoordinate invalidPoint;

        public ConsistentAreaTester(GeometryGraph<TCoordinate> geomGraph)
        {
            this._geomGraph = geomGraph;
        }

        /// <summary>
        /// Returns the intersection point, or <see langword="null" /> if none was found.
        /// </summary>        
        public ICoordinate InvalidPoint
        {
            get { return invalidPoint; }
        }

        public Boolean IsNodeConsistentArea
        {
            get
            {
                /*
                * To fully check validity, it is necessary to
                * compute ALL intersections, including self-intersections within a single edge.
                */
                SegmentIntersector<TCoordinate> intersector = _geomGraph.ComputeSelfNodes(_li, true);

                if (intersector.HasProperIntersection)
                {
                    invalidPoint = intersector.ProperIntersectionPoint;
                    return false;
                }

                _nodeGraph.Build(_geomGraph);
                return IsNodeEdgeAreaLabelsConsistent;
            }
        }

        /// <summary>
        /// Check all nodes to see if their labels are consistent.
        /// If any are not, return false.
        /// </summary>
        private Boolean IsNodeEdgeAreaLabelsConsistent
        {
            get
            {
                for (IEnumerator nodeIt = _nodeGraph.GetNodeEnumerator(); nodeIt.MoveNext();)
                {
                    RelateNode node = (RelateNode) nodeIt.Current;

                    if (!node.Edges.IsAreaLabelsConsistent)
                    {
                        invalidPoint = (ICoordinate) node.Coordinate.Clone();
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Checks for two duplicate rings in an area.
        /// Duplicate rings are rings that are topologically equal
        /// (that is, which have the same sequence of points up to point order).
        /// If the area is topologically consistent (determined by calling the
        /// <c>isNodeConsistentArea</c>,
        /// duplicate rings can be found by checking for EdgeBundles which contain more than one EdgeEnd.
        /// (This is because topologically consistent areas cannot have two rings sharing
        /// the same line segment, unless the rings are equal).
        /// The start point of one of the equal rings will be placed in invalidPoint.
        /// Returns <see langword="true"/> if this area Geometry is topologically consistent but has two duplicate rings.
        /// </summary>
        public Boolean HasDuplicateRings
        {
            get
            {
                for (IEnumerator nodeIt = _nodeGraph.GetNodeEnumerator(); nodeIt.MoveNext();)
                {
                    RelateNode node = (RelateNode) nodeIt.Current;
                    for (IEnumerator i = node.Edges.GetEnumerator(); i.MoveNext();)
                    {
                        EdgeEndBundle eeb = (EdgeEndBundle) i.Current;
                        if (eeb.EdgeEnds.Count > 1)
                        {
                            invalidPoint = eeb.Edge.GetCoordinate(0);
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}