import { useState } from 'react'


import './App.css'

function App() {
  const [user, setUser] = useState(null)

  const setSession = async () => {
    const response = await fetch('/api/set-session', { method: 'POST', credentials: 'include' })
    const data = await response.json()
    console.log(data)
  }

  const getSession = async () => {
    const response = await fetch('/api/get-session', { credentials: 'include' })
    const data = await response.json()
    if (response.ok) {
      setUser(data)
    } else {
      console.log("Error getting session data")
    }
  }

  const clearSession = async () => {
    const response = await fetch('/api/clear-session', { method: 'DELETE', credentials: 'include' })
    const data = await response.json()
    console.log(data)
  }


  return (
    <>
      <div>
        <h1>This is a session demo</h1>
        <button onClick={setSession}>Set Session</button>
      </div>
      <div>
        <h1>Session Data</h1>
        <button onClick={getSession}>Get Session</button>
        <p>{user}</p>
      </div>
      <div>
        <button onClick={clearSession}>Clear Session</button>
      </div>
    </>
  )
}


export default App
