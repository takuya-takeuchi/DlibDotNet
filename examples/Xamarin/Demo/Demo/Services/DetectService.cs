using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Demo.Models;
using Demo.Services.Interfaces;
using DlibDotNet;

namespace Demo.Services
{

    public sealed class DetectService : IDetectService
    {

        #region Fields

        private readonly FrontalFaceDetector _FrontalFaceDetector;

        private readonly ShapePredictor _PosePredictor68Point;

        #endregion

        #region Constructors

        public DetectService()
        {
            var resourcePrefix = "Demo.data.";
            // note that the prefix includes the trailing period '.' that is required
            var files = new [] { "shape_predictor_68_face_landmarks.dat" };
            var assembly = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(DetectService)).Assembly;
            foreach (var file in files)
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), file);
                using var fs = File.Create(path);
                using var stream = assembly.GetManifestResourceStream(resourcePrefix + file);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fs);
            }

            var binPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), files[0]);
            this._FrontalFaceDetector = Dlib.GetFrontalFaceDetector();
            this._PosePredictor68Point = ShapePredictor.Deserialize(binPath);
        }

        #endregion

        #region IDetectService Members

        public DetectResult Detect(byte[] file)
        {
            using var frame = Dlib.LoadPng<RgbPixel>(file);
            var rects = this._FrontalFaceDetector.Operator(frame);

            var faces = new List<Face>();
            foreach (var rect in rects)
            {
                var ret = this._PosePredictor68Point.Detect(frame, new Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom));
                var landmarkTuples = Enumerable.Range(0, (int)ret.Parts)
                                               .Select(index => new FacePoint(ret.GetPart((uint)index), index)).ToArray();
                faces.Add(new Face(landmarkTuples, rect));
            }

            return new DetectResult(frame.Columns, frame.Rows, faces);
        }

        #endregion

    }

}
