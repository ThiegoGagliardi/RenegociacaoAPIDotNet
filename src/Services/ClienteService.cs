using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;
using RenegociacaoAPI.src.mapeamentoDados;
using Microsoft.EntityFrameworkCore;

namespace RenegociacaoAPI.src.Services
{
    public class ClienteService
    {
        private RenegociacaoContext _context;

        public ClienteService(RenegociacaoContext contexto)
        {
            this._context = contexto;            
        }

        public ClienteServiceResultado CriarUmCliente( ClienteCreateDTO novoClienteDTO)
        {
            var clienteLocalizado =  _context.Clientes.FirstOrDefault(c => c.Cpf == novoClienteDTO.Cpf);

            if (!(clienteLocalizado is null)){

                return new ClienteServiceResultado ("Cliente já cadastrado", StatusServiceRetorno.Erro);
            }

            try{

                ClienteMapeamento mapeamento = new ClienteMapeamento(novoClienteDTO);
                Cliente novoCliente = mapeamento.RetornaCliente();

                _context.Clientes.Add(novoCliente);
                _context.SaveChanges();

                mapeamento = new ClienteMapeamento(novoCliente, _context);

                ClienteServiceResultado resultado = new ClienteServiceResultado( "Cliente cadastrado", StatusServiceRetorno.Criado);
                resultado.Clientes.Add(mapeamento.RetornaClienteDTO());

                return resultado;
            } catch (Exception e){
                throw new Exception ($"Erro ao cadastrar cliente, { e.Message }");
            }          
        }

        public ClienteServiceResultado LocalizarClientePorId(int id)
        {
            try{

                var query = _context.Clientes.Where(c => c.Id == id).Include(cliente => cliente.Contratos.Where(contrato => contrato.ClienteId == id));

                Cliente clienteLocalizado = query.FirstOrDefault();

                if (clienteLocalizado is null){
                    return new ClienteServiceResultado($"Cliente  com id {id} não localizado", StatusServiceRetorno.Nao_Localizado);
                }

                ClienteMapeamento mapeamento = new ClienteMapeamento(clienteLocalizado, _context);

                ClienteReadDTO clienteLocalizadoDTO = mapeamento.RetornaClienteDTO();

                var resultado =  new ClienteServiceResultado ("Cliente localizado",StatusServiceRetorno.Localizado);
                
                resultado.Clientes.Add(clienteLocalizadoDTO);

                return resultado;

            } catch (Exception e){
                throw new Exception ($"Erro ao localizar cliente por Id, { e.Message }");
            }
        }

        public ClienteServiceResultado LocalizarCliente()
        {
            try{

                var clientesLocalizado = _context.Clientes.ToList();

                if (clientesLocalizado is null)
                {
                    return new ClienteServiceResultado($"Clientes não cadastrados", StatusServiceRetorno.Erro);
                }

                ClienteServiceResultado resultado =  new ClienteServiceResultado ("Clientes localizados",StatusServiceRetorno.Localizado);                

                foreach (var cliente in clientesLocalizado){

                    ClienteMapeamento mapeamento = new ClienteMapeamento(cliente, _context);
                    resultado.Clientes.Add(mapeamento.RetornaClienteDTO());
                }

                return resultado;

            } catch (Exception e){
                throw new Exception($"Erro ao localizar cliente por Id, { e.Message }");
            }
        }        

        public ClienteServiceResultado AtualizarCliente(int id, ClienteCreateDTO clienteAlteradoDTO)
        {
            try{
                Cliente clienteAlterado = _context.Clientes.FirstOrDefault(c => c.Id == id);

                if (clienteAlterado is null){

                    return new ClienteServiceResultado($"Clientes com id {id} não localizado para atualização", StatusServiceRetorno.Nao_Localizado);
                }

                clienteAlterado.Nome = clienteAlteradoDTO.Nome;
                clienteAlterado.Cpf  = clienteAlteradoDTO.Cpf;

                _context.Clientes.Update(clienteAlterado);
                _context.SaveChanges();

                var resultado = new ClienteServiceResultado($"Cliente com id {id} atualizado", StatusServiceRetorno.Atualizado);
                return resultado;

            } catch (Exception e){

                throw new Exception($"Erro ao atualizar cliente Id {id}, { e.Message }");
            }
        }

        public ClienteServiceResultado DeletarCliente(int id)
        {
            try{
                Cliente clienteDeletado = _context.Clientes.FirstOrDefault(c => c.Id == id);

                if (clienteDeletado is null){

                    return new ClienteServiceResultado($"Clientes com id {id} não localizado para deleção", StatusServiceRetorno.Nao_Localizado);
                }

                _context.Clientes.Remove(clienteDeletado);
                _context.SaveChanges();

                var resultado = new ClienteServiceResultado($"Cliente com id {id} atualizado", StatusServiceRetorno.Deletado);
                return resultado;

            } catch (Exception e){

                throw new Exception($"Erro ao deletar cliente Id {id}, { e.Message }");
            }
        }        
        
    }
}