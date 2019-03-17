﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private DataContext _context;

    //constructor - with datacontext injection
    public ValuesController(DataContext context)
    {
      _context = context;
    }

    // GET api/values
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetValues()
    {
       var values = await _context.Values.ToListAsync();
       return Ok(values);      
    }

    // GET api/values/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetValueAsync(int id)
    {
       var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
       return Ok(value);
    }
    
  }
}