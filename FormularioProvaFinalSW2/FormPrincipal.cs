namespace FormularioProvaFinalSW2
{
    public partial class FormPrincipal : Form
    {
        private readonly UsuarioController _usuarioController;

        public FormPrincipal()
        {
            InitializeComponent();
            _usuarioController = new UsuarioController(new ApplicationDbContext());
            CarregarUsuarios();

            btnAdicionar.Click += BtnAdicionar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnExcluir.Click += BtnExcluir_Click;
        }

        private void CarregarUsuarios()
        {
            var usuarios = _usuarioController.GetUsuarios();
            dataGridViewUsuarios.DataSource = usuarios;
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            var novoUsuario = new Usuario
            {
                Nome = txtNome.Text,
                Senha = txtSenha.Text,
                Status = true
            };

            _usuarioController.PostUsuario(novoUsuario);

            LimparCampos();
            CarregarUsuarios();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                int usuarioId = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["Id"].Value);

                var usuario = _usuarioController.GetUsuario(usuarioId);

                if (usuario != null)
                {
                    using (var formEdicao = new FormEdicaoUsuario(usuario))
                    {
                        var resultado = formEdicao.ShowDialog();

                        if (resultado == DialogResult.OK)
                        {
                            CarregarUsuarios();
                        }
                    }
                }
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                int usuarioId = Convert.ToInt32(dataGridViewUsuarios.SelectedRows[0].Cells["Id"].Value);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Confirmação", MessageBoxButtons.YesNo);

                if (confirmacao == DialogResult.Yes)
                {
                    _usuarioController.DeleteUsuario(usuarioId);

                    CarregarUsuarios();
                }
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtSenha.Text = string.Empty;
        }

        #region Elementos Gráficos
        private Button btnAdicionar;
        private Button btnEditar;
        private Button btnExcluir;
        private TextBox txtNome;
        private TextBox txtSenha;
        private DataGridView dataGridViewUsuarios;
        #endregion
    }
}
}