﻿using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<IResponseResult> CreateClientAsync(ClientRegistrationDto registrationForm)
    {
        if (registrationForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var clientExist = await _clientRepository.AlreadyExistsAsync(x => x.Email == registrationForm.Email);
            if (clientExist == true)
                return ResponseResult.Error("Client with that name already exist");

            await _clientRepository.BeginTransactionAsync();
            var clientEntity = ClientFactory.CreateEntity(registrationForm);
            await _clientRepository.AddAsync(clientEntity);
            bool saveResult = await _clientRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _clientRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _clientRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error creating client :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> DeleteClientAsync(int id)
    {
        try
        {
            var entity = await _clientRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("client not found");

            await _clientRepository.BeginTransactionAsync();
            await _clientRepository.DeleteAsync(x => x.Id == id);
            bool saveResult = await _clientRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _clientRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _clientRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting client :: {ex.Message}");
        }
    }
    

    public async Task<IResponseResult> GetAllClientsAsync()
    {
        try
        {
            var entites = await _clientRepository.GetAllAsync();
            var clients = entites.Select(ClientFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<Client>>.Ok(clients);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving clients");
        }
    }

    public async Task<IResponseResult> GetClientByIdAsync(int id)
    {
        try
        {
            var entity = await _clientRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Client not found");

            var client = ClientFactory.CreateModel(entity);
            return ResponseResult<Client>.Ok(client);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving client");
        }
    }

    public async Task<IResponseResult> UpdateClientAsync(int id, ClientRegistrationDto updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var entityToUpdate = await _clientRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
                return ResponseResult.NotFound("client not found");

            await _clientRepository.BeginTransactionAsync();
            entityToUpdate = ClientFactory.CreateEntity(updateForm, entityToUpdate.Id);
            await _clientRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
            bool saveResult = await _clientRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _clientRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _clientRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating client :: {ex.Message}");
        }
    }
}