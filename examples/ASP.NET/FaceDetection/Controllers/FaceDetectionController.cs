using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DlibDotNet;
using DlibDotNet.Extensions;
using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Image = FaceDetection.Models.Image;

namespace FaceDetection.Controllers
{

    /// <summary>
    /// Get rectangles of face area from specified image
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FaceDetectionController : ControllerBase
    {

        #region Fields

        private readonly ILogger<FaceDetectionController> _Logger;

        #endregion

        #region Constructors

        public FaceDetectionController(ILogger<FaceDetectionController> logger)
        {
            this._Logger = logger;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// Returns an enumerable collection of face location correspond to all faces in specified image.
        /// </summary>
        [Route("~/api/[controller]/Locations")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FaceArea>> Locations([FromBody] Image image)
        {
            try
            {
                var areas = new List<FaceArea>();

                using (var ms = new MemoryStream(image.Data))
                using (var bitmap = (Bitmap)System.Drawing.Image.FromStream(ms))
                using (var matrix = bitmap.ToMatrix<BgrPixel>())
                using (var faceDetector = Dlib.GetFrontalFaceDetector())
                {
                    var dets = faceDetector.Operator(matrix);
                    foreach (var r in dets)
                        areas.Add(new FaceArea { Left = r.Left, Top = r.Top, Right = r.Right, Bottom = r.Bottom });
                }

                return Ok(areas.ToArray());
            }
            catch (Exception e)
            {
                this._Logger.LogError($"[{nameof(this.Locations)}] {e.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion

    }

}
