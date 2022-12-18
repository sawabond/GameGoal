import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Home from './pages/Home/Home';
import Registration from './pages/Registration/Registration';
import './App.scss';
import Login from './pages/Login/Login';
import { userContext } from './Contexts/userContext';
import ImportMembers from './pages/ImportMembers/ImportMembers';
import CreateAchievementSystem from './pages/CreateAchivSystem/CreateAchievementSystem';
import AchievementSystems from './pages/AchievementsSystems/AchievementsSystems';
import Achievement from './pages/Achievement/Achievement';
import CreateAchievements from './pages/CreateAchievements/CreateAchievements';
import { ProtectedRoute } from './routes/ProtectedRoute';
import Error from './pages/Error/Error';
import useAuthProvider from './hooks/useAuthProvider';
function App() {
  const userProvider = useAuthProvider();

  return (
    <div className="App">
      <userContext.Provider value={userProvider}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/registr" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          <Route path="*" element={<Error />} />
          <Route element={<ProtectedRoute />}>
            <Route path="/import" element={<ImportMembers />} />
            <Route
              path="/create-system"
              element={<CreateAchievementSystem />}
            />
            <Route path="/system" element={<AchievementSystems />} />
            <Route path="/system-achievements" element={<Achievement />} />
            <Route
              path="/create-achievements"
              element={<CreateAchievements />}
            />
          </Route>
        </Routes>
      </userContext.Provider>
    </div>
  );
}

export default App;
