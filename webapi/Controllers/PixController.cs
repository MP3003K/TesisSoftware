using API.Controllers.Base;
using Application.Features.Pix.Commands;
using Application.Features.Pix.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class PixController : BaseController
{
    /// <summary>
    /// Get pix by id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response<PixDto>>> GetPix(int id)
    {
        return Ok(await Mediator.Send(new GetPixQuery() {Id = id}));
    }

    /// <summary>
    /// List all pix
    /// </summary>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response<IList<PixDto>>>> ListAllPix()
    {
        return Ok(await Mediator.Send(new ListPixQuery()));
    }

    /// <summary>
    /// Add new pix
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property BankCode is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property BankCode Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Key is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Key Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Bank not found by BankId
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Bank Cannot Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Pix already exist
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "Already exist a key with this value:???"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<PixDto>>> AddPix([FromBody] AddPixRequest request)
    {
        return Created("", await Mediator.Send(request));
    }

    /// <summary>
    /// Delete pix
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If Entity not found by id
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Pix Cannot Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpDelete]
    [Route("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<PixDto>>> DeletePix([FromRoute] string key)
    {
        return Ok(await Mediator.Send(new DeletePixRequest() {Key = key}));
    }
}