import React, { useContext, useState } from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { Link } from 'react-router-dom';
import GroupAddIcon from '@mui/icons-material/GroupAdd';
import LanguageIcon from '@mui/icons-material/Language';
import NoteAddIcon from '@mui/icons-material/NoteAdd';
import { userContext } from '../Contexts/userContext';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';
import Tooltip from '@mui/material/Tooltip';
import useLogout from '../hooks/useLogout';
import '../components/Header.scss';
import i18n from 'i18next';
import { useTranslation } from 'react-i18next';

export default function PrimarySearchAppBar() {
  const [anchorEl, setAnchorEl] = useState(null);
  const [languageAnchorEl, setLanguageAnchorEl] = useState(null);

  const isMenuOpen = Boolean(anchorEl);
  const isLanguageMenuOpen = Boolean(languageAnchorEl);

  const { user } = useContext(userContext);
  const logOut = useLogout();

  const { t } = useTranslation();

  const handleProfileMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleLanguageMenuOpen = (event) => {
    setLanguageAnchorEl(event.currentTarget);
  };

  const handleLanguageMenuClose = () => {
    setLanguageAnchorEl(null);
  };

  const userMenuId = 'primary-search-account-menu';
  const renderAccountMenu = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      id={userMenuId}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      open={isMenuOpen}
      onClose={handleMenuClose}
    >
      {user && user ? (
        <div className="menu-login">
          <MenuItem onClick={logOut}>{t('LOGOUT')}</MenuItem>
        </div>
      ) : (
        <div className="menu-unlogin">
          <Link to={'/registr'}>
            <MenuItem onClick={handleMenuClose}>{t('REGISTRATION')}</MenuItem>
          </Link>
          <Link to={'/login'}>
            <MenuItem onClick={handleMenuClose}>{t('LOG_IN')}</MenuItem>
          </Link>
        </div>
      )}
    </Menu>
  );

  const languageMenuId = 'primary-search-language-menu';
  const renderLanguageMenu = (
    <Menu
      anchorEl={languageAnchorEl}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      id={languageMenuId}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      open={isLanguageMenuOpen}
      onClose={handleLanguageMenuClose}
    >
      <div className="menu-language">
        <MenuItem
          onClick={() => {
            i18n.changeLanguage('en');
            localStorage.setItem('locale', 'en');
            handleLanguageMenuClose();
          }}
        >
          EN
        </MenuItem>
        <MenuItem
          onClick={() => {
            i18n.changeLanguage('uk');
            localStorage.setItem('locale', 'uk');
            handleLanguageMenuClose();
          }}
        >
          UK
        </MenuItem>
      </div>
    </Menu>
  );

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        style={{ backgroundColor: '#1976d2', color: 'white' }}
      >
        <Toolbar>
          <Box sx={{ flexGrow: 1 }} />

          <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
            {user ? (
              <Box>
                <Tooltip title="Import users">
                  <IconButton size="large">
                    <Link to={'/import'}>
                      <GroupAddIcon style={{ color: 'white' }} />
                    </Link>
                  </IconButton>
                </Tooltip>

                <Tooltip title={t('CREATE_ACHIEVEMENT_SYSTEM')}>
                  <IconButton size="large">
                    <Link to={'/create-system'}>
                      <NoteAddIcon style={{ color: 'white' }} />
                    </Link>
                  </IconButton>
                </Tooltip>

                <Tooltip title="Achievement systems">
                  <IconButton
                    size="large"
                    aria-label="show 4 new mails"
                    color="inherit"
                  >
                    <Link to={'/system'}>
                      <EmojiEventsIcon style={{ color: 'white' }} />
                    </Link>
                  </IconButton>
                </Tooltip>
              </Box>
            ) : (
              ''
            )}

            <IconButton
              size="large"
              edge="end"
              aria-label="language of current user"
              aria-controls={languageMenuId}
              aria-haspopup="true"
              onClick={handleLanguageMenuOpen}
              color="inherit"
            >
              <LanguageIcon />
            </IconButton>

            <IconButton
              size="large"
              edge="end"
              aria-label="account of current user"
              aria-controls={userMenuId}
              aria-haspopup="true"
              onClick={handleProfileMenuOpen}
              color="inherit"
            >
              <AccountCircle />
            </IconButton>
          </Box>
        </Toolbar>
      </AppBar>
      {renderLanguageMenu}
      {renderAccountMenu}
    </Box>
  );
}
