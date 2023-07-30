using API.Controllers.Base;
using Application.Features.Transaction.Command;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TransactionController : BaseController
{
    /// <summary>
    /// Transfer Value
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
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
    ///         If Entity Pix not found by Account Code
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Pix Cannot Be Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Value less than zero
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Sent Value Needs Be Greater Than Zero"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<Boolean>>> AddDeposit([FromBody] AddTransferRequest request)
    {
        return Created("", await Mediator.Send(request));
    }
}