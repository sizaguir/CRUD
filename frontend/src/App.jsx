import { useState, useEffect } from 'react'
import './App.css'

function App() {
  const [personas, setPersonas] = useState([])
  const [nombre, setNombre] = useState('')
  const [apellido, setApellido] = useState('')
  const [editandoId, setEditandoId] = useState(null)
  

  const API_URL = 'http://localhost:5095/personas'

  useEffect(() => {
    traerPersonas()
  }, [])

  const traerPersonas = async () => {
    const respuesta = await fetch(API_URL)
    const datos = await respuesta.json()
    setPersonas(datos)
  }

  const guardarPersona = async (e) => {
    e.preventDefault()

    if (editandoId) {
      await fetch(API_URL + '/' + editandoId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: editandoId, nombre, apellido })
      })
      setEditandoId(null)
    } else {
      await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nombre, apellido })
      })
    }

    setNombre('')
    setApellido('')
    traerPersonas()
  }

  const prepararEdicion = (persona) => {
    setNombre(persona.nombre || persona.Nombre)
    setApellido(persona.apellido || persona.Apellido)
    setEditandoId(persona.id || persona.Id)
  }

  const borrarPersona = async (id, nombre) => {
    const confirmado = window.confirm('¿Seguro que quieres eliminar a ' + nombre + '?')
    if (confirmado) {
      await fetch(API_URL + '/' + id, { method: 'DELETE' })
      traerPersonas()
    }
  }

  return (
    <div style={{ padding: '20px' }}>
      <h1>CRUD con React y .NET 8</h1>
      <form onSubmit={guardarPersona} style={{ marginBottom: '20px' }}>
        <input
          type="text"
          value={nombre}
          onChange={(e) =>
            setNombre(e.target.value)}
          placeholder="Nombre"
          required
        />
        <input
          type="text"
          value={apellido}
          onChange={(e) => setApellido(e.target.value)}
          placeholder="Apellido"
          required
        />
        <button type="submit">{editandoId ? 'Actualizar' : 'Agregar'}</button>
      </form>
      <ul>
        {personas.map((persona) => (
          <li key={persona.id}>
            {persona.nombre} {persona.apellido}
            <div className="botones">
              <button onClick={() => prepararEdicion(persona)}>
                Editar
              </button>

              <button onClick={() => borrarPersona(persona.id, persona.nombre)}>
                Borrar
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App