import React, { useState, useMemo } from 'react';
import { Route, Routes } from 'react-router-dom';
import Home from './pages/Home/Home';
import Registration from './pages/Registration/Registration';
import './App.scss';
import Login from './pages/Login/Login';
import { AuthContext } from './hooks/useAuth';
import ImportMembers from './pages/ImportMembers/ImportMembers';
import CreateAchievementSystem from './pages/CreateAchivSystem/CreateAchievementSystem';
import AchievementSystems from './pages/AchievementsSystems/AchievementsSystems';
import Achievement from './pages/Achievement/Achievement';
import CreateAchievements from './pages/CreateAchievements/CreateAchievements';
function App() {
  const [user, setUser] = useState(null);
  const providerUser = useMemo(() => ({ user, setUser }), [user, setUser]);
  return (
    <div className="App">
      <AuthContext.Provider value={providerUser}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/registr" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          <Route path="/import" element={<ImportMembers />} />
          <Route path="/create-system" element={<CreateAchievementSystem />} />
          <Route path="/system" element={<AchievementSystems />} />
          <Route path="/system-achievements" element={<Achievement />} />
          <Route path="/create-achievements" element={<CreateAchievements />} />
        </Routes>
      </AuthContext.Provider>
    </div>
  );
}

export default App;
